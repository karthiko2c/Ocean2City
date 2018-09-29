import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { ApiConfiguration } from 'src/app/webapi/api-configuration';
import { ItemService } from 'src/app/webapi/services';

@Injectable()
export class ItemServiceApp {

    constructor(private apiItemService: ItemService, private http: HttpClient, private apiConfig: ApiConfiguration) { }

    getItemList(): Observable<any> {
        return this.apiItemService.ApiItemItemsGet().map(x => (x));
    }

    getItemById(id): Observable<any> {
        return this.apiItemService.ApiItemDetailsGet(id).map(x => (x));
    }

    getItemByCategory(id): Observable<any> {
        return this.apiItemService.ApiItemGetItemsByCategoryGet(id).map(x => (x));
    }

    addItem(uri, item, image: any): Observable<any> {
        const url = this.apiConfig.rootUrl + uri;
        const formdata = new FormData();
        formdata.append('item', JSON.stringify(item));
        formdata.append('image', image);
        return this.http.post(url, formdata, { observe: 'response' }).map(x => (x));
    }

    updateItem(uri, item, image: any): Observable<any> {
        const url = this.apiConfig.rootUrl + uri;
        const formdata = new FormData();
        formdata.append('item', JSON.stringify(item));
        formdata.append('image', image);
        return this.http.put(url, formdata, { observe: 'response' }).map(x => (x));
    }
}
