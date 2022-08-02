import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PrimeNGConfig } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { LANGUAGES } from '../../constants';
import { changeLanguage } from '../../helpers/i18n';

@Injectable({
  providedIn: 'root'
})
export class I18nService {
  constructor(
    private config: PrimeNGConfig
  ) { }

  changeLanguage(language: string) {
    changeLanguage(language, this.config);
    this.language = language;
  }

  get language(): string {
    return localStorage.getItem(environment.keys.languageStorage) ?? LANGUAGES.tr;
  }

  set language(value: string) {
    localStorage.setItem(environment.keys.languageStorage, value);
  }
}
