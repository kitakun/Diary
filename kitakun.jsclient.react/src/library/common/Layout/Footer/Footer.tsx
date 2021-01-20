import React from 'react';
import './Footer.scss';
// Locals
import Container from '../Container/Container';

const currentYear = new Date().getFullYear();

function Footer() {
    return (
        <footer className="app-footer">
            <Container>
                <span>© {currentYear} - WriteItDown.ru</span>
            </Container>
        </footer>
    );
}

export default Footer;
