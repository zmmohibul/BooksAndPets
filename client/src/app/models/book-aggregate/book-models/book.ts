import { Publisher } from '../publisher-models/publisher';
import { Author } from '../author-models/author';
import { Language } from '../language-models/language';
import { Department } from '../../product-aggregate/department-models/department';
import { Category } from '../../product-aggregate/category-models/category';
import { PriceList } from '../../product-aggregate/price-list-models/priceList';

export interface Book {
  id: number;
  name: string;
  authors: Author[];
  priceList: PriceList[];
  publicationDate: string;
  highlightText: string;
}
