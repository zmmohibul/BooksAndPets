import { Publisher } from '../publisher-models/publisher';
import { Author } from '../author-models/author';
import { Language } from '../language-models/language';
import { Department } from '../../product-aggregate/department-models/department';
import { Category } from '../../product-aggregate/category-models/category';
import { Price } from '../../product-aggregate/price-list-models/price';

export interface Book {
  id: number;
  name: string;
  mainPictureUrl: string;
  authors: Author[];
  priceList: Price[];
  publicationDate: string;
  highlightText: string;
}
