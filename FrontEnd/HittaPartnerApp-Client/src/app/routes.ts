import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { ListsComponent } from "./lists/lists.component";
import { LoginComponent } from "./login/login.component";
import { MemberDetailComponent } from "./members/member-detail/member-detail.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ResetpasswordComponent } from "./resetpassword/resetpassword.component";
import { AuthGuard } from "./_guards/auth.guard";
import { MemberDetailResolver } from "./_resolvers/member-detail.resolver";

export const appRoutes:Routes=[
    
    {path:'',component:HomeComponent},
    {path:'login',component:LoginComponent},
    {path:'reset',component:ResetpasswordComponent},
    {path:'members',component:MemberListComponent,canActivate:[AuthGuard]},
    {path:'member/:id',component:MemberDetailComponent,canActivate:[AuthGuard],resolve:{user:MemberDetailResolver}},
    {path:'lists',component:ListsComponent,canActivate:[AuthGuard]},
    {path:'messages',component:MessagesComponent,canActivate:[AuthGuard]},
    {path:'**',redirectTo:'',pathMatch:'full'}
];