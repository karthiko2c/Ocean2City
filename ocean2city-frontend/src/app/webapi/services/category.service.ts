/* tslint:disable */
import { Injectable } from '@angular/core';
import {
  HttpClient, HttpRequest, HttpResponse,
  HttpHeaders, HttpParams } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { IResult } from '../models/iresult';
@Injectable({
  providedIn: 'root',
})
class CategoryService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  ApiCategoryCategoriesGetResponse(): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Category/Categories`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult;
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @return Success
   */
  ApiCategoryCategoriesGet(): Observable<IResult> {
    return this.ApiCategoryCategoriesGetResponse().pipe(
      map(_r => _r.body)
    );
  }

  /**
   * @param id Identifier of the Category.
   * @return Success
   */
  ApiCategoryDetailsGetResponse(id?: string): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (id != null) __params = __params.set('id', id.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Category/Details`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult;
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @param id Identifier of the Category.
   * @return Success
   */
  ApiCategoryDetailsGet(id?: string): Observable<IResult> {
    return this.ApiCategoryDetailsGetResponse(id).pipe(
      map(_r => _r.body)
    );
  }

  /**
   * @return Success
   */
  ApiCategoryAddCategoryPostResponse(): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Category/AddCategory`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult;
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @return Success
   */
  ApiCategoryAddCategoryPost(): Observable<IResult> {
    return this.ApiCategoryAddCategoryPostResponse().pipe(
      map(_r => _r.body)
    );
  }

  /**
   * @return Success
   */
  ApiCategoryUpdateCategoryPutResponse(): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Category/UpdateCategory`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult;
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @return Success
   */
  ApiCategoryUpdateCategoryPut(): Observable<IResult> {
    return this.ApiCategoryUpdateCategoryPutResponse().pipe(
      map(_r => _r.body)
    );
  }
}

module CategoryService {
}

export { CategoryService }
