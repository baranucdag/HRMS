import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customDate'
})
export class CustomDatePipe extends DatePipe implements PipeTransform {

  override transform(value: any, format?: any): any {
    return super.transform(value, format ?? "dd.MM.yyyy");
  }

}
