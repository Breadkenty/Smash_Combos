import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { authHeader, isLoggedIn, getUser } from '../auth'
import { useHistory } from 'react-router'

export function Reports() {
  const loggedInUser = getUser()
  const history = useHistory()

  const [users, setUsers] = useState([])

  function getReports() {
    fetch('/api/Users', {
      method: 'GET',
      headers: { 'content-type': 'application/json', ...authHeader() },
    })
      .then(response => response.json())
      .then(apiData => {
        setUsers(apiData)
      })
  }

  function sumReports(user) {
    let totalReports = 0
    user.combos.forEach(
      combo =>
        (totalReports =
          totalReports +
          combo.reports.filter(report => report.dismiss === false).length)
    )
    user.comments.forEach(
      comment =>
        (totalReports =
          totalReports +
          comment.reports.filter(report => report.dismiss === false).length)
    )
    return totalReports
  }

  useEffect(() => {
    if (!isLoggedIn() || loggedInUser.userType < 2) {
      history.push('/forbidden')
    } else {
      getReports()
    }
  }, [])

  return (
    <div className="reports">
      <header>
        <h1>Reports</h1>
      </header>

      <section>
        <p>Reports</p>
        <p>User</p>
        {users
          .filter(user => sumReports(user) > 0)
          .map(user => (
            <>
              <p>{sumReports(user)}</p>
              <Link to={`/user/${user.displayName}`}>{user.displayName}</Link>
            </>
          ))}
      </section>
    </div>
  )
}