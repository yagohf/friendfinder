import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Autenticacao } from '../_models/autenticacao';
import { Router, ActivatedRoute } from '@angular/router';
import { MensagensService } from '../_services/mensagens.service';
import { EnumMensagem } from '../_models/enums/enum.mensagem';

@Component({
  selector: 'app-acesse',
  templateUrl: './acesse.component.html',
  styleUrls: ['./acesse.component.css']
})
export class AcesseComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private mensagemService: MensagensService) { }

  ngOnInit() {
    this.formLogin = this.formBuilder.group({
      usuario: ['', Validators.required],
      senha: ['', Validators.required]
    });

    //Setar URL de retorno após o login.
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  //Getter para facilitar acesso aos controles.
  get f() { return this.formLogin.controls; }

  submeterLogin() {
    this.submitted = true;

    if (this.formLogin.invalid) {
      return;
    }

    this.logando = true;

    let autenticacao: Autenticacao = {
      login: this.f.usuario.value,
      senha: this.f.senha.value
    };

    this.authenticationService.login(autenticacao)
      .subscribe(
        resultado => {
          console.log(resultado);
          if (resultado && resultado.token) {
            this.router.navigate([this.returnUrl]);
          }
          else {
            this.mensagemService.enviarMensagem('Usuário ou senha inválidos.', EnumMensagem.ERRO);
            this.logando = false;
          }
        },
        error => {
          console.log(error);
          this.logando = false;
        });
  }

  logando: boolean = false;
  formLogin: FormGroup;
  returnUrl: string;
  submitted: boolean = false;
}
