import React from 'react'

function Home({name}) {

    return (

        <div>
            {name ? `Hi, ${name}!` : "You are not logged in!!!"}
        </div>
    )
}

export default Home
