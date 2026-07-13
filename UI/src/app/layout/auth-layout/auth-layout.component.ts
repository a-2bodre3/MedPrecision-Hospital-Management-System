import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LogoComponent } from '../../shared/components/logo/logo.component';
import { FooterAuthLayoutComponent } from './footer-auth-layout/footer-auth-layout.component';

@Component({
  selector: 'app-auth-layout',
  imports: [RouterOutlet, LogoComponent, FooterAuthLayoutComponent],
  templateUrl: './auth-layout.component.html',
  styleUrl: './auth-layout.component.scss',
})
export class AuthLayoutComponent {}
