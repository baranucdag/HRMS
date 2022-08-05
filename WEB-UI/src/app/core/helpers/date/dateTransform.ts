export function dateTransformForBackend(value:Date) {
    let day = value.getDate();
    let month = value.getMonth();
    let year = value.getFullYear();
    var a =  `${++month}.${day}.${year} `;
    let NewDate = new Date(a);
    return NewDate;
}