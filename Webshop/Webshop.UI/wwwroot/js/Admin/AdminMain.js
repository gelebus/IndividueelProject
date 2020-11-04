var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products: [],
        productViewModel: {
            id: "0",
            name: "ProductName",
            value: "1,10",
            description: "ProductDescription"
        }
    },
    methods:{
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(result => { console.log(result); this.products = result.data; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productViewModel)
                .then(result => { console.log(result); this.products.push(result.data); })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false });
        }
    }
});