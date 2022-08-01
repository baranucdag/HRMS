import { ILanguage } from "../../models/i18n"

export const LANGUAGES = {
    tr: "tr-TR",
    en: "en-US",
    fr: "fr-FR",
}

export const LANGUAGE_LIST: ILanguage[] = [
    {
        name: "Türkçe",
        code: LANGUAGES.tr
    },
    {
        name: "English",
        code: LANGUAGES.en
    }
]