export class CartItem {
  productId: number;
  name: string;
  pictureUrl: string;
  measureTypeId: number;
  measureOptionId: number;
  measureOption: string;
  unitPrice: number;
  quantityInStock: number;
  quantity = 0;

  constructor(
    productId: number,
    name: string,
    pictureUrl: string,
    measureTypeId: number,
    measureOptionId: number,
    measureOption: string,
    unitPrice: number,
    quantity: number,
    quantityInStock: number,
  ) {
    this.productId = productId;
    this.name = name;
    this.pictureUrl = pictureUrl;
    this.measureTypeId = measureTypeId;
    this.measureOptionId = measureOptionId;
    this.measureOption = measureOption;
    this.unitPrice = unitPrice;
    this.quantityInStock = quantityInStock;
    this.quantity = quantity;
  }
}
