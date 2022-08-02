declare global {
    interface String {
        toCapitalize(): String;
        toPascalCase(): String;
        concat(value: string): String;
    }
}

String.prototype.toCapitalize = function (): String {
    const str = this;
    if (!str)
        return "";

    if (str.length === 0)
        return str;

    return str[0].toLocaleUpperCase() + str.substring(1, str.length).toLocaleLowerCase().replace("ı","i");
}

String.prototype.toPascalCase = function (): String {
    const str = this;
    if (!str)
        return "";

    if (str.length === 0)
        return str;

    const parts = str.split(" ");
    let newStr = "";

    newStr = parts.map(m => m.toCapitalize()).join(" ");

    return newStr;
}

export default String;