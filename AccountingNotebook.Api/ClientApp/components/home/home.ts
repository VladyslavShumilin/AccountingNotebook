import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Transaction } from '../../models/Transaction';



@Component
export default class HomeComponent extends Vue {
    transactions: Transaction[] = [];

    mounted() {
        fetch('api/transaction/getAllTransactions')
            .then(response => response.json() as Promise<Transaction[]>)
            .then(data => {
                this.transactions = data;
            });
    }
}
