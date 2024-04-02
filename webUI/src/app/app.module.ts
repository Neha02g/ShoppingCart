import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsListComponent } from './Components/products-list/products-list.component';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { ProfileComponent } from './Components/profile/profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CartItemsComponent } from './Components/cart-items/cart-items.component';
import { DeliveryDetailsComponent } from './Components/delivery-details/delivery-details.component';
import { TransactionComponent } from './Components/transaction/transaction.component';
import { HeroComponent } from './Components/hero/hero.component';
import { ViewOrderComponent } from './Components/view-order/view-order.component';
import { ConfirmOrderComponent } from './Components/confirm-order/confirm-order.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { PrivacyComponent } from './Components/privacy/privacy.component';
import { SupportComponent } from './Components/support/support.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductsListComponent,
    LoginComponent,
    SignupComponent,
    ProfileComponent,
   CartItemsComponent,
   DeliveryDetailsComponent,
   TransactionComponent,
   HeroComponent,
   ViewOrderComponent,
   ConfirmOrderComponent,
   ForgotPasswordComponent,
   PrivacyComponent,
   SupportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {


}
