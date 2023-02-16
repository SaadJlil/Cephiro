import styled from 'styled-components';

interface ButtonContentProps
{
    direction?: "column" | "row"
}

export const ButtonContent = styled.div<ButtonContentProps>`
    display: flex;
    flex-direction: ${props => props.direction == "row" ? 'row' : 'column'};
    gap: 0.5rem;
`

