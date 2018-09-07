export interface ItemViewModel {
    itemId?: string;
    itemName?: string;
    aliasName?: string;
    description?: string;
    recipe?: string;
    priceWithoutClean?: number;
    priceWithClean?: number;
    discount?: number;
    image?: string;
    isAvailable?: boolean;
    category?: string;
}
