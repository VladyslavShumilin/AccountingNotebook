import Vue from 'vue';
import { Route } from 'vue-router';
import { Component } from 'vue-property-decorator';
import { Transaction } from '../../models/Transaction';

@Component
export default class TransactionDetailsComponent extends Vue {
    transaction: {} = {};

    mounted() {
        fetch('api/transaction/getTransactionById?id=' + this.$route.params.transactionId)
            .then(response => response.json() as Promise<Transaction>)
            .then(data => {
                this.transaction = data;
            });
    }
}
