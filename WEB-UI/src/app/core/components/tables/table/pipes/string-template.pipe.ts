import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'stringTemplate'
})
export class StringTemplatePipe implements PipeTransform {

  transform(value: any, object: any): any {
    for (var prop in object) {
      if (object.hasOwnProperty(prop)) {
        value = value.toString().replace(new RegExp('{{' + prop + '}}', "g"), object[prop]);
      }
    }

    return value;
  }

}
