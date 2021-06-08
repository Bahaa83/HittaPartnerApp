import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateAgo',
  pure: true
})
export class DateAgoPipe implements PipeTransform {

  transform(value: any, ...args: any): any {
      if (value) {
          const seconds = Math.floor((+new Date() - +new Date(value)) / 1000);
         
          if (seconds <= 5) // För mindre än 5 sekunder sedan visas som 'Aktiv nu'
              return 'Aktiv nu';
          if (seconds < 29) //för mindre än 30 sekunder sedan visas som 'Precis nu'
              return 'Precis nu';

          const intervals:any = {
              'År': 31536000,
              'Månad': 2592000,
              'Vecka': 604800,
              'Dag': 86400,
              'Timme': 3600,
              'Minut': 60,
              'Sekund': 1
          };
          let counter;
          for (const i in intervals) {
              counter = Math.floor(seconds / intervals[i]);
              if (counter > 0)
                  if (counter === 1) {
                      return counter + ' ' + i + ' sen'; // singular (1 day ago)
                  } else {
                      return counter + ' ' + i + ' sen'; // plural (2 days ago)
                  }
          }
      }
      return value;
  }

}