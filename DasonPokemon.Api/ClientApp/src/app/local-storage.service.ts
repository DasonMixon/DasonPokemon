import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  public get<T>(key : string) : T | null {
    const data = localStorage.getItem(key);
    if (data) {
      return data as unknown as T;
    } else {
      return null;
    }
  }

  public getOrFetch<T>(key : string, fetch : Observable<T>) : Observable<T> {
    let existingItem = localStorage.getItem(key);
    if (existingItem) {
      return of<T>(existingItem as unknown as T);
    } else {
      return fetch.pipe(
        switchMap(response => {
            this.set(key, response);
            return of<T>(response as T);
        })
      );
    }
  }

  public set(key : string, data : any) {
    localStorage.setItem(key, data);
  }
}
