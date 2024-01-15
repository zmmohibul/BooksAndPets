import { PagedData } from '../../utils/PagedData';
import { Book } from './book';
import { Author } from '../author-models/author';
import { Publisher } from '../publisher-models/publisher';

export interface BookData {
  data: PagedData<Book>;
  authors: Author[];
  publishers: Publisher[];
}
