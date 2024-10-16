import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

 
  transform(itemss: any[], searchQuery: string): any[] {
    if (!itemss || !searchQuery) {
      return itemss;
    }

    const lowerCaseQuery = searchQuery.toLowerCase();

    return itemss.filter(item =>
      item.name.toLowerCase().includes(lowerCaseQuery) ||
      item.urlHandle.toLowerCase().includes(lowerCaseQuery)
    );
  }

}
