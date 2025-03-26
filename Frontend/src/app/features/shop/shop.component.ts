import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/products';
import { ShopService } from '../../backend/shop.service';
import { MatCard, MatCardContent } from '@angular/material/card';
import { ProductComponent } from './product/product.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductComponent,
    MatButton,
    MatMenuModule,
    MatIconModule,
    MatSelectionList,
    MatListOption,
    MatPaginatorModule,
    FormsModule,
  ],
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css'],
})
export class ShopComponent implements OnInit {
  //injects
  private DialogService = inject(MatDialog);
  private shopService = inject(ShopService);

  //fields
  title = 'Frontend';
  products?: pagination<Product>;
  shopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => (this.products = response),
      error: (error) => console.log(error),
    });
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.GetProducts();
  }

  handlePageChange(event: PageEvent) {
    debugger;
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.GetProducts();
  }

  GetProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => (this.products = response),
      error: (error) => console.log(error),
    });
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.GetProducts();
    }
  }
  openFiltersDialog() {
    this.DialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types,
      },
    })
      .afterClosed()
      .subscribe({
        next: (result) => {
          if (result) {
            debugger;
            this.shopParams.brands = result.selectedBrands;
            this.shopParams.types = result.selectedTypes;
            this.shopParams.pageNumber = 1;
            this.GetProducts();
          }
        },
      });
  }
}
