import { HttpParams } from '@angular/common/http';

export class HttpQueryParameter {
  pageNumber: number = 1;
  pageSize: number = 5;
  categoryId: number | null = null;
  authorsId: number[] | null = null;
  publishersId: number[] | null = null;

  getQueryString() {
    let queryString = '';
    queryString += this.pageNumber + '-';

    queryString += this.pageSize;

    if (this.categoryId) {
      queryString += '-' + this.categoryId;
    }

    if (this.authorsId) {
      queryString += '-' + this.authorsId.join(',') + ',';
    }

    if (this.publishersId) {
      queryString += '-' + this.publishersId.join(',') + ',';
    }

    return queryString;
  }

  getHttpParamsObject() {
    let params = new HttpParams();
    params = params.append('pageNumber', this.pageNumber);

    params = params.append('pageSize', this.pageSize);

    if (this.categoryId) {
      params = params.append('categoryId', this.categoryId);
    }

    if (this.authorsId) {
      for (let authorId of this.authorsId) {
        params = params.append('authorsId', authorId);
      }
    }

    if (this.publishersId) {
      for (let publisherId of this.publishersId) {
        params = params.append('publishersId', publisherId);
      }
    }

    return params;
  }
}
