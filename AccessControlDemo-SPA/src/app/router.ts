import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from '_guard/auth.guard';
import { HomeComponent } from './home/home.component';
import { DetailComponent } from './detail/detail.component';

export const appRoutes: Routes = [
    { path: '', component: LoginComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'home', component: HomeComponent, resolve: {} },
            { path: 'detail', component: DetailComponent, resolve: {} },
        ]
    },

    { path: '**', redirectTo: '', pathMatch: 'full' }
];
