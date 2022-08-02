import { HttpClient } from "@angular/common/http";
import { TranslateService } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";

import { PrimeNGConfig } from "primeng/api";
import { Observable } from "rxjs";
import { I18nService } from "../../services/i18n";

let translate: TranslateService;

export const initializeI18n = (translateService: TranslateService, i18nService: I18nService) => {
    return (): Promise<void> => {
        return new Promise<void>((resolve, reject) => {
            translate = translateService;
            translate.setDefaultLang(i18nService.language);
            setTimeout(() => {
                resolve();
            }, 300);
        });
    }
}

// AoT requires an exported function for factories
export const HttpLoaderFactory = (http: HttpClient) => {
    return new TranslateHttpLoader(http, "../../../assets/i18n/", ".json");
}

export const changeLanguage = (language: string, config: PrimeNGConfig) => {
    translate.setDefaultLang(language);
    translate.get('primeng').subscribe(res => config.setTranslation(res));
}

export const T = (key: any) => {
    return translate.instant(key);
}