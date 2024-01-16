export class CartItem {
  productId: number;
  pictureUrl: string;
  name: string;
  measureOption: string;
  unitPrice: number;
  quantityInStock: number;
  quantity = 0;

  constructor(
    productId: number,
    name: string,
    pictureUrl: string,
    measureOption: string,
    unitPrice: number,
    quantity: number,
    quantityInStock: number,
  ) {
    this.productId = productId;
    this.pictureUrl = pictureUrl;
    this.name = name;
    this.measureOption = measureOption;
    this.unitPrice = unitPrice;
    this.quantityInStock = quantityInStock;
    this.quantity = quantity;
  }
}
