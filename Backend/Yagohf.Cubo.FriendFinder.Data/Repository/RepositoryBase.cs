﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yagohf.Cubo.FriendFinder.Data.Context;
using Yagohf.Cubo.FriendFinder.Data.Interface.Query;
using Yagohf.Cubo.FriendFinder.Data.Interface.Repository;
using Yagohf.Cubo.FriendFinder.Infrastructure.Extensions;
using Yagohf.Cubo.FriendFinder.Infrastructure.Paging;
using Yagohf.Cubo.FriendFinder.Model.Entidades;

namespace Yagohf.Cubo.FriendFinder.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : EntidadeBase
    {
        protected FriendFinderContext _context;

        public RepositoryBase(FriendFinderContext context)
        {
            this._context = context;
        }

        public async Task AtualizarAsync(T entidade)
        {
            this._context.Entry<T>(entidade).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            this._context.Entry<T>(entidade).State = EntityState.Detached;
        }

        public async Task<int> ContarAsync(IQuery<T> query)
        {
            var queryPreparada = this.PrepararQuery(query);
            return await queryPreparada.CountAsync();
        }

        public async Task InserirAsync(T entidade)
        {
            await this._context.Set<T>().AddAsync(entidade);
            await this._context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            T entidade = await this._context.Set<T>().FindAsync(id);
            this._context.Set<T>().Remove(entidade);
            await this._context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(T entidade)
        {
            await this.ExcluirAsync(entidade.Id);
        }

        public async Task<IEnumerable<T>> ListarAsync(IQuery<T> query)
        {
            return await this.PrepararQuery(query).ToListAsync();
        }

        public async Task<Listagem<T>> ListarPaginandoAsync(IQuery<T> query, int pagina, int qtdRegistrosPorPagina)
        {
            var qtdRegistros = this.ContarAsync(query);
            var registros = this.PrepararQuery(query, pagina, qtdRegistrosPorPagina).ToListAsync();

            return new Listagem<T>(await registros, new Paginacao(pagina, await qtdRegistros, qtdRegistrosPorPagina));
        }

        private IQueryable<T> PrepararQuery(IQuery<T> query)
        {
            IQueryable<T> queryPrincipal = this._context.Set<T>().AsNoTracking();
            if (query != null)
            {
                if (query.Includes.Any())
                {
                    // Montar um IQueryable<T> com todos os includes explícitos.
                    queryPrincipal = query.Includes
                    .Aggregate(this._context.Set<T>().AsQueryable(),
                        (current, include) => current.Include(include));
                }

                if (query.IncludeStrings.Any())
                {
                    // Adicionar os includes de strings.
                    queryPrincipal = query.IncludeStrings
                        .Aggregate(queryPrincipal,
                            (current, include) => current.Include(include));
                }

                //Tratar where.
                if (query.Criteria != null)
                {
                    foreach (var criteria in query.Criteria)
                    {
                        queryPrincipal = queryPrincipal.Where(criteria);
                    }
                }

                //Tratar ordenação.
                if (query.OrderBy != null)
                {
                    queryPrincipal = PrepararOrdenacao(query, queryPrincipal);
                }
            }

            return queryPrincipal;
        }

        private IQueryable<T> PrepararOrdenacao(IQuery<T> query, IQueryable<T> queryPrincipal)
        {
            if (query.OrderBy != null && query.OrderBy.Any())
            {
                IOrderedQueryable<T> queryOrdenar = queryPrincipal as IOrderedQueryable<T>;
                for (int i = 0; i < query.OrderBy.Count; i++)
                {
                    var order = query.OrderBy[i];
                    if (order.Descending)
                    {
                        queryOrdenar = i == 0 ? queryOrdenar.OrderByDescending(order.Expression) : queryOrdenar.ThenByDescending(order.Expression);
                    }
                    else
                    {
                        queryOrdenar = i == 0 ? queryOrdenar.OrderBy(order.Expression) : queryOrdenar.ThenBy(order.Expression);
                    }
                }

                return queryOrdenar as IQueryable<T>;
            }

            return queryPrincipal;
        }

        private IQueryable<T> PrepararQuery(IQuery<T> query, int pagina, int qtdRegistrosPorPagina)
        {
            IQueryable<T> queryPreparada = this.PrepararQuery(query);
            return queryPreparada.PrepararQueryParaPaginar(pagina, qtdRegistrosPorPagina);
        }

        public async Task<T> SelecionarUnicoAsync(IQuery<T> query)
        {
            return await this.PrepararQuery(query).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ListarTodosAsync()
        {
            return await this.ListarAsync(null);
        }

        public async Task<bool> ExisteAsync(IQuery<T> query)
        {
            return await this.PrepararQuery(query).AnyAsync();
        }
    }
}
