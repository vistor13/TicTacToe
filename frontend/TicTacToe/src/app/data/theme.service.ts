import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly themeKey = 'theme';

  constructor(@Inject(DOCUMENT) private document: Document) {
    this.loadTheme();
  }

  setTheme(theme: string) {
    this.document.documentElement.setAttribute('data-theme', theme);
    document.cookie = `${this.themeKey}=${theme}; path=/; max-age=${60 * 60 * 24 * 365}`;
  }

  getTheme(): string {
    const match = document.cookie.match(new RegExp('(^| )' + this.themeKey + '=([^;]+)'));
    return match ? match[2] : 'dark';
  }

  private loadTheme() {
    const savedTheme = this.getTheme();
    this.setTheme(savedTheme);
  }
}
