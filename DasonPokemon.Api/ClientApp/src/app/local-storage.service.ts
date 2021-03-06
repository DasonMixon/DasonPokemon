import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  public get<T>(key : string) : T | null {
    const data = localStorage.get(key);
    if (data) {
      return data;
    } else {
      return null;
    }
  }

  public getOrFetch<T>(key : string, fetch : Observable<T>) : Observable<T> {
    let data = localStorage.get(key);
    if (data) {
      return data;
    } else {
      data = fetch.pipe(
        switchMap(response => {
            this.set(key, response);
            return of<T>(response as T);
        })
      );
      if (data) {
        localStorage.setItem(key, data);
        return data;
      } else {
        return of<T>();
      }
    }
  }

  public set(key : string, data : any) {
    localStorage.setItem(key, data);
  }
}
