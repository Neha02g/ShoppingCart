import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { AppComponent } from './app.component';
import { SignupComponent } from './Components/signup/signup.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { ProductsListComponent } from './Components/products-list/products-list.component';
import { CartItemsComponent } from './Components/cart-items/cart-items.component';
import { DeliveryDetailsComponent } from './Components/delivery-details/delivery-details.component';
import { TransactionComponent } from './Components/transaction/transaction.component';
import { HeroComponent } from './Components/hero/hero.component';
import { ViewOrderComponent } from './Components/view-order/view-order.component';
import { ConfirmOrderComponent } from './Components/confirm-order/confirm-order.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { PrivacyComponent } from './Components/privacy/privacy.component';
import { SupportComponent } from './Components/support/support.component';

const routes: Routes = [
  {
    path:'login',
    component:LoginComponent

  },
  {
    path:'',
    component:HeroComponent

  },
  {
    path:'signup',
    component:SignupComponent

  },
  {
    path:'profile',
    component:ProfileComponent

  },
  {
    path:'search',
    component:ProductsListComponent

  },
  {
    path:'cart-items',
    component:CartItemsComponent

  },
  {

  path:'delivery-details',
  component:DeliveryDetailsComponent
},
{

path:'transaction',
component:TransactionComponent
}, {
  path:'viewOrder',
  component:ViewOrderComponent

}, {
  path:'confirmOrder',
  component:ConfirmOrderComponent

},{
  path:'forgotPassword',
  component: ForgotPasswordComponent
},
{
  path:'privacy',
  component:PrivacyComponent

},
  {
    path:'support',
    component:SupportComponent

  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
