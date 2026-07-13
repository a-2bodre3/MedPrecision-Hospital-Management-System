import { Component, inject, OnInit } from '@angular/core';
import { LucideMail } from '@lucide/angular';
import { RouterOutlet } from '@angular/router';
import { HlmToasterImports } from '@spartan-ng/helm/sonner';
import { LookupsState } from './core/state/Lookups.store';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HlmToasterImports],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private lookupsStore = inject(LookupsState);
  ngOnInit() {
    this.lookupsStore.getLookups();
  }
}
