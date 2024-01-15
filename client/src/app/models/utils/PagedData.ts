export interface PagedData<T> {
  count: number;
  currentPage: number;
  totalPages: number;
  pageSize: number;
  items: T[];
}
