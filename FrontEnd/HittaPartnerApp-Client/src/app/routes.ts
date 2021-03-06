import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { ListsComponent } from "./lists/lists.component";
import { LoginComponent } from "./login/login.component";
import { MemberDetailComponent } from "./members/member-detail/member-detail.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ResetpasswordComponent } from "./resetpassword/resetpassword.component";
import { AuthGuard } from "./_guards/auth.guard";
import { PreventUnsavedChangesGuard } from "./_guards/prevent-unsaved-changes.guard";
import { ListResolver } from "./_resolvers/lists.resolver";
import { MemberDetailResolver } from "./_resolvers/member-detail.resolver";
import { MemberEditResolver } from "./_resolvers/member-edit.resolver";
import { MemberListResolver } from "./_resolvers/member-list.resolver";
import { MessageResolver } from "./_resolvers/message.resolver";

export const appRoutes:Routes=[
    
    {path:'',component:HomeComponent},
    {
        path:'',
        runGuardsAndResolvers:'always',
        canActivate:[AuthGuard],
        children:[
            {path:'members',component:MemberListComponent,resolve:{
                users:MemberListResolver
            }},
            {path:'memberedit',component:MemberEditComponent,resolve:{
                user:MemberEditResolver
            },canDeactivate:[PreventUnsavedChangesGuard]},
            {path:'member/:id',component:MemberDetailComponent,resolve:{
                user:MemberDetailResolver
            }},
         
            {path:'lists',component:ListsComponent,resolve:{
                users:ListResolver
            }},
            {path:'messages',component:MessagesComponent,resolve:{
                messages:MessageResolver
            }}
        ]
    },
    {path:'login',component:LoginComponent},
    {path:'reset',component:ResetpasswordComponent},
    {path:'**',redirectTo:'',pathMatch:'full'}
];