var app = new Vue({
    el: '#app',
    data: {
        loading:false,
        products: [],
        currentSelectedProduct: {
            id: 0,
            description: "test",
            stock: []
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
            this.currentSelectedProduct.id = product.id; //doesnt work yet
            this.currentSelectedProduct.description = product.description; //doesnt work yet
            this.currentSelectedProduct.stock = product.stock; //doesnt work yet
            this.currentStock.productId = product.Id;
            console.log(product);
        },
        updateStock() {

        },
        createStock() {
            axios.post('/Admin/stocks',this.currentStock)
                .then(result => { console.log(result); this.products.Stock = [...this.products.Stock, result.data]; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        }
    }
})
