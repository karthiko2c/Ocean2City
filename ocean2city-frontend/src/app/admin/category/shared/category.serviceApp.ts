import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { CategoryService } from 'src/app/webapi/services';
import { ApiConfiguration } from 'src/app/webapi/api-configuration';

@Injectable()
export class CategoryServiceApp {

    constructor(private apiCategoryService: CategoryService, private http: HttpClient, private apiConfig: ApiConfiguration) { }

    getCategoryList(): Observable<any> {
        return this.apiCategoryService.ApiCategoryCategoriesGet().map(x => (x));
    }

    getCategoryById(id): Observable<any> {
        return this.apiCategoryService.ApiCategoryDetailsGet(id).map(x => (x));
    }

    addCategory(uri, category, image: any): Observable<any> {
        const url = this.apiConfig.rootUrl + uri;
        const formdata = new FormData();
        formdata.append('category', JSON.stringify(category));
        formdata.append('image', image);
        return this.http.post(url, formdata, { observe: 'response' }).map(x => (x));
    }

    updateCategory(uri, category, image: any): Observable<any> {
        const url = this.apiConfig.rootUrl + uri;
        const formdata = new FormData();
        formdata.append('category', JSON.stringify(category));
        formdata.append('image', image);
        return this.http.put(url, formdata, { observe: 'response' }).map(x => (x));
    }
}
