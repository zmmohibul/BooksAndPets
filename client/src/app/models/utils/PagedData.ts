export class PagedData<T> {
  count: number = 0;
  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 5;
  items: T[] = [];
}
