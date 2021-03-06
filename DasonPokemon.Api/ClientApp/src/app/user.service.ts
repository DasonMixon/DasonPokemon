import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { Observable, of } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userId : string | null = null;

  constructor(private http : HttpClient, private storage : LocalStorageService) {
    const storageUser = this.storage.get('user') as User;
    if (storageUser !== null && storageUser !== undefined) {
      this.userId = storageUser.id;
    }
  }

  public setUserId(id : string) {
    this.userId = id;
  }

  public getUser(id : string | null = this.userId) : Observable<User> {
    if (id === null)
      return of<User>();
    
    return this.storage.getOrFetch('user', this.http.get<User>(`api/user/${id}`));
  }
}
