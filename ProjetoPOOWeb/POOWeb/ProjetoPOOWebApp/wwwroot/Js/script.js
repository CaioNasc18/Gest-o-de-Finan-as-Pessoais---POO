async function fetchCategories() {
    const response = await fetch("/categorias");
    return await response.json();
}

async function fetchTransactions() {
    const response = await fetch("/transacoes");
    return await response.json();
}

function renderTransactions(items, categories) {
    const tbody = document.querySelector('#tx-table tbody');
    tbody.innerHTML = '';
    items.forEach(function(t) {
        const cat = categories.find(c => c.id === t.categoriaId);
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${t.id}</td>
            <td>${t.descricao}</td>
            <td>${t.valor.toFixed(2)}</td>
            <td>${new Date(t.data).toLocaleString()}</td>
            <td>${cat ? cat.nome : '—'}</td>
            <td>${t.tipo === 0 ? 'Receita' : 'Despesa'}</td>
        `;
        tbody.appendChild(row);
    });
}

async function loadPage() {
    const categories = await fetchCategories();
    const txs = await fetchTransactions();
    const sel = document.getElementById('category-select');
    sel.innerHTML = '<option value="0">-- Sem categoria --</option>';
    categories.forEach(function(c) {
        const opt = document.createElement('option');
        opt.value = c.id;
        opt.textContent = c.nome;
        sel.appendChild(opt);
    });
    renderTransactions(txs, categories);
}

document.getElementById('tx-form').addEventListener('submit', async function (e) {
    e.preventDefault();
    const desc = document.getElementById('desc').value || '';
    const value = parseFloat(document.getElementById('value').value);
    const categoriaId = parseInt(document.getElementById('category-select').value, 10);
    const tipo = document.getElementById('type-select').value === "Receita" ? 0 : 1;

    const body = { descricao: desc, valor: value, categoriaId, tipo };

    const res = await fetch("/transacoes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body)
    });

    if (res.ok) {
        document.getElementById("tx-form").reset();
        await loadPage();
    } else {
        const err = await res.json();
        alert("Erro: " + (err.error || "Pedido inválido"));
    }
});

window.addEventListener('load', loadPage);
