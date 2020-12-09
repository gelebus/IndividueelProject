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
            var vm = this.currentSelectedProduct.stock.map(x => {
                    return {
                        id: Number(x.id),
                        productId: Number(this.currentSelectedProduct.id),
                        quantity: Number(x.quantity),
                        description: x.description
                    }
            })
            axios.put('/Admin/stocks', vm)
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
