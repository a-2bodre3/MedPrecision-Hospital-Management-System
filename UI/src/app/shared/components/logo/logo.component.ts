import { Component, input } from '@angular/core';
import { LucideSquarePlus } from '@lucide/angular';

@Component({
  selector: 'app-logo',
  imports: [LucideSquarePlus],
  templateUrl: './logo.component.html',
  styleUrl: './logo.component.scss',
})
export class LogoComponent {
  //=========================================
  //=============input=======================
  //=========================================
  public hasIcon = input(true);
}
