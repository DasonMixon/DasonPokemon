import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { Observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient, private storage : LocalStorageService) {
  }

  public getUser(id : string) : Observable<User> {
    return this.storage.getOrFetch('user', this.http.get<User>(`api/user/${id}`));
  }
}
