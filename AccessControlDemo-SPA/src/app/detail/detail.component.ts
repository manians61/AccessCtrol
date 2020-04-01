import { Component, OnInit } from '@angular/core';
import { UserService } from '_service/user.service';
import { User } from '_models/user';
import { PermissionObject } from '_models/permissionObject';
import { AlertifyService } from '_service/alertify.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  constructor(private userService: UserService, private alertify: AlertifyService) { }
  currentUser: User;
  permissionObjects: PermissionObject[];
  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.getPermissionObjectsByUser(this.currentUser.user_ID, 'gp000010').subscribe(
      (obs: PermissionObject[]) => {
        this.permissionObjects = obs;
      },
      error => {
        this.alertify.error('wrong');
      }
    );
  }

}
