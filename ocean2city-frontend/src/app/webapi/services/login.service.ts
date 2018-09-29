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
import { UserLoginViewModel } from '../models/user-login-view-model';
@Injectable({
  providedIn: 'root',
})
class LoginService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param loginViewModel Login view model.
   * @return Success
   */
  ApiLoginLoginUserPostResponse(loginViewModel?: UserLoginViewModel): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = loginViewModel;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Login/LoginUser`,
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
   * @param loginViewModel Login view model.
   * @return Success
   */
  ApiLoginLoginUserPost(loginViewModel?: UserLoginViewModel): Observable<IResult> {
    return this.ApiLoginLoginUserPostResponse(loginViewModel).pipe(
      map(_r => _r.body)
    );
  }
}

module LoginService {
}

export { LoginService }
