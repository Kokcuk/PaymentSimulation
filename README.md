# PaymentSimulation
Bank: New bank account added 8888
Bank: $500 added to 8888
Merchant: new good added 'Carrot' price: 39,99
Customer: new purchase goodId: 12 quantity:3
Merchant: new order added Carrot #3
Payment aggregator: new session added id: 74271610-8a0f-450e-a0cc-4d5825f2fe20 amount: $119,97, waiting for payment
Customer: merchant redirect to payment aggregator, sessionId: 74271610-8a0f-450e-a0cc-4d5825f2fe20 Success
Customer: payment aggregator payment attempt
Bank: Charged $119,97 from account 8888
Payment aggregator: charging form bank account 8888 result: Success
Merchant: payment status update session: 74271610-8a0f-450e-a0cc-4d5825f2fe20 state: Paid
Customer: payment aggregator redirect to payment aggregator, sessionId:  Success 74271610-8a0f-450e-a0cc-4d5825f2fe20
Customer: merchant result Failt Session expired or invalid
Customer: money left: $380,03
