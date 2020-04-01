import { Component, OnInit } from '@angular/core';
import { UserService } from '_service/user.service';
import { PermissionGroup } from '_models/permissionGroup';
import { User } from '_models/user';
import { AlertifyService } from '_service/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: User;
  text = '';
  constructor(private userService: UserService, private alertify: AlertifyService) { }
  groups: PermissionGroup[];
  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.getGroups();
  }
  getGroups() {
    this.userService.getPermissionGroupsbyUser(this.currentUser.user_ID).subscribe(
      (res: PermissionGroup[]) => {
        this.groups = res;
      },
      error => {
        this.alertify.error('wrong');
      }
    );
  }

}
