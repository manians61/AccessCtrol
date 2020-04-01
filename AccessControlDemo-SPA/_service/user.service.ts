import { Injectable } from '@angular/core';
import { PermissionGroup } from '_models/permissionGroup';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpParams, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'auth/permission/';
  constructor(private http: HttpClient) { }
  getPermissionGroupsbyUser(id): Observable<PermissionGroup[]> {
    console.log(id);
    return this.http.get<PermissionGroup[]>(this.baseUrl + id);
  }



  getPermissionObjectsByUser(userID?, groupID?) {
    let params = new HttpParams();
    if (userID != null && groupID != null) {
      params = params.append('user_ID', userID);
      params = params.append('group_ID', groupID);
    }
    return this.http.post(this.baseUrl + 'userPermission', { params });
  }
}
