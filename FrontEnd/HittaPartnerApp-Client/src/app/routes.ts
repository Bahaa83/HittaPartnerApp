import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { ListsComponent } from "./lists/lists.component";
import { LoginComponent } from "./login/login.component";
import { MemberListComponent } from "./member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";

export const appRoutes:Routes=[
    {path:'',component:HomeComponent},
    {path:'home',component:HomeComponent},
    {path:'login',component:LoginComponent},
    {path:'members',component:MemberListComponent},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent},
    {path:'**',redirectTo:'home',pathMatch:'full'}
];