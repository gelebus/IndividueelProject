var app = new Vue({
    el: '#app',
    data: {
        loading:false,
        products: [],
        currentSelectedProduct: {
            id: 0,
            description: "something",
            stock:[]
        },
        currentStock: {
            productId: 0,
            quantity: 12,
            description: "size"
        }
    },
    mounted() {
        this.getStock(); 
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get('/Admin/stocks')
                .then(result => { console.log(result); this.products = result.data; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        selectProduct(product) {
            this.currentSelectedProduct.id = product.id;
            this.currentSelectedProduct.description = product.description;
            this.currentSelectedProduct.stock = product.stock;
            this.currentStock.productId = Number(product.id);
        },
        updateStock() {
            this.loading = true;
            axios.put('/Admin/stocks', {
                stock: this.currentSelectedProduct.stock.map(x => {
                    return {
                        stockId: Number(x.stockId),
                        description: x.description,
                        productId: Number(this.currentSelectedProduct.id),
                        quantity: Number(x.quantity)
                    }
                })
            })
            .then(result => { console.log(result); })
            .catch(error => { console.log(error); })
            .then(() => { this.loading = false; });
        },
        removeStock(id, index) {
            this.loading = true;
            axios.delete('/Admin/stocks/' + id)
                .then(result => { console.log(result); this.currentSelectedProduct.stock.splice(index, 1) })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        createStock() {
            this.loading = true;
            axios.post('/Admin/stocks',this.currentStock)
                .then(result => { console.log(result); this.currentSelectedProduct.stock = [...this.currentSelectedProduct.stock, result.data]; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        }
    }
})
