import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormControl, FormGroup, FormArray } from '@angular/forms';


@Component({
  selector: 'add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  data: any;
  record: any = {};
  records: any;
  jsonObject: any;
  date: any;

  productsListIndex: number = -1;

  productForm = new FormGroup({
    creator: new FormControl('', []),
    date: new FormControl(),
    comments: new FormControl(),
    inputNum: new FormControl(),
    inputPrice: new FormControl(),
    inputQuantity: new FormControl(),
    inputSum: new FormControl(),
    rows: new FormArray([])
  });
  
  constructor(private http: HttpClient)
  {  
    http.get('https://localhost:5001/api/product/').subscribe(data => {
      this.records = data;
      console.log(data);
      });

      

      this.date = new Date().toLocaleString();

      this.productForm.controls['date'].setValue(this.date);
      this.productForm.controls['inputNum'].setValue(this.getNum(this.date));
  }

  getNum(date: any)
  {
    //return date.toString().replaceAll('/', '').replaceAll(',', '').replaceAll(' ', '').replaceAll(':', '').slice(0, 14)
    return Math.round((Math.random()*100));
  }

  onChange()
  {
    this.productForm.controls['inputSum'].setValue(this.getSum());
  }

  getSum():Number
  {
    return (Number)(this.productForm.controls['inputQuantity'].value) * (Number)(this.productForm.controls['inputPrice'].value)
  }

  addProduct()
  {
    this.record.creator = this.productForm.controls['creator'].value;
    this.record.date = this.productForm.controls['date'].value;
    this.record.comments = this.productForm.controls['comments'].value;

    let products = [];
    let product;

    for(let row of this.getRows())
    {
          product = {
                      creator: this.record.creator,
                      date: this.record.date,
                      comments: this.record.comments,
                      num: (Number)(row.num),
                      price: (Number)(row.price),
                      quantity: (Number)(row.quantity),
                    }

          products.push(product);
      }

      product = JSON.stringify(products);

      console.log('Product', product);
      console.log('Products', products);

      this.http.post<any>('https://localhost:5001/api/product/', products, { headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' }) })
      .subscribe(response => {
          console.log('response ==>', response);
        }
      )
  }

  addRow()
  {
    (this.productForm.get('rows') as FormArray).push(new FormControl({
        num: this.productForm.controls['inputNum'].value,
        price: this.productForm.controls['inputPrice'].value,
        quantity: this.productForm.controls['inputQuantity'].value,
        sum: this.productForm.controls['inputSum'].value
      }));

      this.productForm.controls['inputNum'].setValue(this.getNum(new Date().toLocaleString()));
  }

  getRows()
  {
    return this.productForm.controls['rows'].value;
  }
}