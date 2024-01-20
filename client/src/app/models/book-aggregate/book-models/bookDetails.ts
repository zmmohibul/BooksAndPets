import { Publisher } from '../publisher-models/publisher';
import { Author } from '../author-models/author';
import { Language } from '../language-models/language';
import { Department } from '../../product-aggregate/department-models/department';
import { Category } from '../../product-aggregate/category-models/category';
import { Price } from '../../product-aggregate/price-list-models/price';
import { ProductPicture } from '../../product-aggregate/product-models/productPicture';

export interface BookDetails {
  highlightText: string;
  publisher: Publisher;
  authors: Author[];
  language: Language;
  pageCount: number;
  publicationDate: string;
  isbn: string;
  id: number;
  name: string;
  description: string;
  department: Department;
  categories: Category[];
  pictures: ProductPicture[];
  priceList: Price[];
}
