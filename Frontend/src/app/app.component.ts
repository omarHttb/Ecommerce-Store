import { Component } from '@angular/core';
import { ShopComponent } from './features/shop/shop.component';
import { HeaderComponent } from './layout/header/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'main battle tank';
}
