import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class BookPage extends Component {
    static displayName = BookPage.name;

    constructor(props) {
        super(props);
        this.state = {
            books: [],
            loading: true,
            newBook: {
                title: '',
                author: '',
               
            },
        };
    }

    componentDidMount() {
        this.fetchBooks();
    }

    fetchBooks() {
        fetch('http://localhost:44436/book')
            .then(response => response.json())
            .then(data => {
                this.setState({ books: data, loading: false });
            });
    }

    createBook() {
        const { newBook } = this.state;

        fetch('http://localhost:44436/book', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newBook),
        })
            .then(response => {
                if (response.status === 201) {
                    this.fetchBooks(); 
                    this.setState({ newBook: {} });
                } else {
                    // add error handling here
                }
            });
    }

    deleteBook(id) {
        fetch(`http://localhost:44436/book/${id}`, {
            method: 'DELETE',
        })
            .then(response => {
                if (response.status === 204) {
                    this.fetchBooks(); 
                } else {
                    // Handle error here
                }
            });
    }

    handleInputChange(event) {
        const { name, value } = event.target;
        this.setState(prevState => ({
            newBook: {
                ...prevState.newBook,
                [name]: value,
            },
        }));
    }

    render() {
        const { books, loading, newBook } = this.state;

        return (
            <div>
                <h1>Book Page</h1>

                <div>
                    <h2>Create New Book</h2>
                    <input
                        type="text"
                        placeholder="Title"
                        name="title"
                        value={newBook.title}
                        onChange={e => this.handleInputChange(e)}
                    />
                    <input
                        type="text"
                        placeholder="Author"
                        name="author"
                        value={newBook.author}
                        onChange={e => this.handleInputChange(e)}
                    />
                    {/* Add other input fields for book attributes */}
                    <button onClick={() => this.createBook()}>Create</button>
                </div>

                {loading ? (
                    <div>Loading...</div>
                ) : (
                    <>
                        <h2>Books</h2>
                        <ul>
                            {books.map(book => (
                                <li key={book.id}>
                                    <Link to={`/book/${book.id}`}>
                                        {book.title} by {book.author}
                                    </Link>
                                    <button onClick={() => this.deleteBook(book.id)}>Delete</button>
                                </li>
                            ))}
                        </ul>
                    </>
                )}
            </div>
        );
    }
}
